using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hotcakes.Modules.ProcessImagesModule.Components
{
    public class Constants
    {
        // TODO: Make the path localizable
        public const string LOCALIZATION_FILE_PATH = "~/DesktopModules/ProcessImagesModule/App_LocalResources/View.ascx.resx";

        public const string SETTINGS_SCHEDULER_ENABLED = "Hcc.ImportImages.SchedulerEnabled";
        public const string SETTINGS_SCHEDULER_ID = "Hcc.ImportImages.ScheduleJobId";

        public const string SCHEDULED_JOB_TYPE = "Hotcakes.Modules.ProcessImagesModule.Services.Scheduler.ProcessImagesScheduledJob, Hotcakes.Modules.ProcessImagesModule";
        public const string SCHEDULED_JOB_NAME = "Hotcakes Commerce: Import Images";
    }
}