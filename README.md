# Hotcakes Commerce: ProcessImagesModule

This project is NOT MEANT FOR PRODUCTION. You should only use this as a reference or proof of concept to show how 
product images may be imported at any time programmatically.  It's based upon the image import section in the documentation below.

Related Documentation:  [https://hotcakescommerce.zendesk.com/hc/en-us/articles/205426535-Product-Import](https://hotcakescommerce.zendesk.com/hc/en-us/articles/205426535-Product-Import)

## About Hotcakes Commerce

[Hotcakes Commerce](https://hotcakes.org) is an e-commerce solution that empowers developers and designers alike to be able to actually fulfill 
client requirements when building an online store.  We believe that e-commerce should be easier, for EVERYONE on your team.

---

## Getting Started: End-Users

**Prerequisite:**  You must already have a CMS ready to use this with, and a superuser account on the site.

1. Download [the latest release](https://github.com/HotcakesCommerce/ProcessImagesModule/releases/latest) and save it somewhere on your computer.
2. Login using your superuser account.
3. Use the control panel to navigate to Host > Extensions.
4. Click the "Install Extension" button, and a popup installation wizard will appear.
5. Follow the steps in the installation wizard.
6. After installation, add the module to a page and use it (preferrably a security-protected page).

## Getting Started: Developers

1. You should create a website folder where your project will be. This can be anywhere on your system. For example `C:\Websites\MyProjectName\`  
2. Clone the repository into that folder  
3. Create a new folder in the root:  _Website_  (Example:  `C:\Websites\MyProjectName\Website\` )  
4. Update IIS and your local HOSTS file to point to your local development URL, such as [http://ProcessImagesModule.loc](http://ProcessImagesModule.loc)  
5. Install the CMS into the website folder (see version info below).  
6. Install Hotcakes Commerce into the CMS.  
7. Run the Hotcakes Commerce "Getting Started" wizard.  
8. Add sample products, or add some of your own.  
9. Build this project in Debug mode.  
10. "Install" the ProcessImages module using the Create New Module > Create from Manifest. (You can also install the package you created when building in Release mode.)  
11. If it's not on a page already, add this module to a new or existing page.  
12. Play with the module.  

---

## Dependencies

### CMS

This solution is currently being built against DNN 09.02.02.

[Download Now](https://hotcakescommerce.zendesk.com/hc/en-us/articles/208602886-Latest-Supported-CMS-Release)

Be sure that you get the file permissions properly assigned to the folders when you install DNN.

### Visual Studio Extensions

The following Visual Studio 2019 extensions are currently installed and being used in my environment, but are not 
necessary to work on the project.  There are more, but these are the only ones that are relevant to this project.

* Guidinserter2
* Microsoft ASP.NET and Web Tools
* NuGet Package Manager
* ReSharper (not free, except to active open source developers)
* UIMap Toolbox
* Web Essentials

---

## Have an update to this?

Here's the steps to help update this.

1. Create an issue.
2. Collaborate.
3. Fork the project.
4. Create a branch.
5. Write your code.
6. Test your code.
7. Create a pull request.
8. Hotcakes staff will review the pull request and take any necessary actions.

## How to Build

While there continues to be only a single module here, you can safely build from the Solution or Project level.

When you first begin using this project, you should first build using __DEBUG__ mode, discussed below.

### What happens when I build?

Building in __DEBUG__ mode will compile the project/solution as you'd expect, but an MS Build script will also 
move the module files into the appropriate Website\DesktopModules\ folder as well.  

Building in __RELEASE__ mode will _not_ move the project files, but it will package the module 
in an Install and Source package that can be used to install on another Hotcakes site for testing or deployment.

`~\Website\Install\Module\`

This is VERY important to know.  This module has a Module.Build file that properly maps it's 
files that need to be moved into the Website folder.

[https://github.com/upendoventures/generator-upendodnn](https://github.com/upendoventures/generator-upendodnn)

## Debugging

Debugging should be done using the "Attach to Process" method found in Visual Studio.

## Solution Architecture

You'll find a website and database backup in the Assets folder.  This should be used as your starting point, as defined in 
the Getting Started section above.

### Configuration Files Solution Folder

This solution folder contains the common configuration files for the entire solution, such as the web.config.

### Modules Solution Folder

This contains the various module projects that will be placed onto pages.

### Website Project

The website project is only used for reference.  Any core code changes to the CMS itself are forbidden.
