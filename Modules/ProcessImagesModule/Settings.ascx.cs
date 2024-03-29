﻿/*
' Copyright (c) 2020 Upendo Ventures, LLC
'  All rights reserved.
' 
' Permission is hereby granted, free of charge, to any person obtaining a copy 
' of this software and associated documentation files (the "Software"), to deal 
' in the Software without restriction, including without limitation the rights 
' to use, copy, modify, merge, publish, distribute, sublicense, and/or sell 
' copies of the Software, and to permit persons to whom the Software is 
' furnished to do so, subject to the following conditions:
' 
' The above copyright notice and this permission notice shall be included in all 
' copies or substantial portions of the Software.
' 
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR 
' IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
' FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE 
' AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER 
' LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, 
' OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE 
' SOFTWARE.
*/

using System;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Instrumentation;
using DotNetNuke.Services.Exceptions;
using Hotcakes.Modules.ProcessImagesModule.Components;
using Hotcakes.Modules.ProcessImagesModule.Services.Scheduler;

namespace Hotcakes.Modules.ProcessImagesModule
{
    public partial class SettingsView : ProcessImagesModuleSettingsBase
    {
        private static readonly ILog Logger = LoggerSource.Instance.GetLogger(typeof(SettingsView));

        #region Base Method Implementations

        /// <summary>
        /// LoadSettings loads the settings from the Database and displays them
        /// </summary>
        public override void LoadSettings()
        {
            try
            {
                fldSettings.Visible = (UserInfo.IsSuperUser);
                divMessage.Visible = (!UserInfo.IsSuperUser);

                if (UserInfo.IsSuperUser)
                {
                    var ctlScheduler = new DnnScheduleController();
                    var blnEnabled = SchedulerEnabled;  // return the setting from module settings

                    if (!SchedulerEnabled && ScheduledJobId > Null.NullInteger && ctlScheduler.ScheduledJobExists(ScheduledJobId))
                    {
                        // if the scheduled job exists already, override the logic here to correctly reflect the status
                        // this can happen if someone manually creates the scheduled job
                        var scheduledJob = ctlScheduler.GetScheduledJob(ScheduledJobId);
                        blnEnabled = (scheduledJob.Enabled);
                    }

                    chkScheduledJob.Checked = blnEnabled;
                }
            }
            catch (Exception exc) // module failed to load
            {
                Logger.Error(exc.Message, exc);
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        /// <summary>
        /// UpdateSettings saves the modified settings to the Database
        /// </summary>
        public override void UpdateSettings()
        {
            try
            {
                var ctlSchedule = new DnnScheduleController();
                var ctlModule = new ModuleController();

				ctlModule.UpdateModuleSetting(ModuleId, Constants.SETTINGS_SCHEDULER_ENABLED, chkScheduledJob.Checked.ToString());

                if (chkScheduledJob.Checked)
                {
                    var scheduleJobId = Null.NullInteger; // local value not related to base settings

                    if (ScheduledJobId == Null.NullInteger)
                    {
                        // one has not been created yet (because it's not in the module settings)
                        scheduleJobId = ctlSchedule.CreateScheduledJob();
                    }

                    if (ScheduledJobId > Null.NullInteger)
                    {
                        // this should run if the scheduled job was previously created at some point, but since been disabled
                        ctlSchedule.EnableScheduledJob(ScheduledJobId);
                    }
                }

                if (!chkScheduledJob.Checked && ScheduledJobId > Null.NullInteger)
                {
                    // the schedule job was previously created so disable it
                    ctlSchedule.DisableScheduledJob(ScheduledJobId);
                }

                // synchronize the module settings
                ModuleController.SynchronizeModule(ModuleId);
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        #endregion
    }
}