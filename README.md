### Requirements
* .NET 4.5
* SQL Server 2012 or higher
* NodeJS 7.10.1 (needed for webpack, will explain later why webpack is needed)

### Setup
The `server` folder contains the backend written using ASP.NET Web API. Do the following steps:
* Open the solution in Visual Studio and restore its nuget packages.
* Open the Web.config in JobPortal.Web project and set the connection string (the name of the connection string is "DbConnectionString"). The database does not have to exist.
* In Visual Studio, in the package manager console run `update-database -ProjectName:"JobPortal.DataAccess" -StartUpProjectName:"JobPortal.Web"` which will run all the migrations, setup the database and populate it with sample data.
* Open the project settings of the "JobPortal.Web" project and remember the port of the project url.

The `client` folder contains the AngularJS code and the webpack setup. Do the following steps:
* In the client folder, create a .env file and copy the contents of .env.example into it.
* Set the BACKEND_SERVER_PORT setting to the backend project url port in the .env file.
* Set WEBPACK_DEV_SERVER_PORT setting to any port you wish in the .env file (the webpack dev server will be listening on this port) .
* Run `npm install`

### To run the application in development mode
* In the client folder run `npm run start:dev` which will start the webpack dev server at http://localhost:WEBPACK_DEV_SERVER_PORT,
* Start debugging the ASP.NET WEB API (the project url port and the .env BACKEND_SERVER_PORT should match, because the webpack dev server is proxying all backend requests to our web api project)
* Visit http://localhost:WEBPACK_DEV_SERVER_PORT to start the development

### To run the application in production mode
* In the client folder run `npm run build:prod` which will create the minified version of the assets and copy them to the JobPortal.Web/Content/dist folder.
* Start the web api server WITHOUT debugging (you can set debug="false" in the Web.config of the JobPortal.Web project).
* Visit http://localhost:BACKEND_SERVER_PORT to start the app in production mode.

### Why was webpack used?
The client side code is written using javascript ES6 and SCSS which should be compiled to javascript ES5 and CSS. In development mode, the webpack dev server does hot module replacement (https://webpack.js.org/concepts/hot-module-replacement/) which allows to change the styles and see the results without refreshing the app (in development mode change one of the scss files and observe how the changes are reflected without refreshing the app). Also, when the app is built for production, the javascript and css files are minified and content hashes are added to their names. In addition to that, angularjs component templates are cached for production.

### Code structure
#### Client-side code
The client side code is in the `client` folder. The `config` folder contains the webpack configuration for both development and production. The `src` folder contains the application code written according to this styleguide (https://github.com/toddmotto/angularjs-styleguide). It is important to note that all vendor libraries are imported in the `src/app/vendor.js` file and all polyfills are imported in the `src/app/polyfills.js` file.

#### Server-side code
The server-side code is in the `server` folder. The solution is organized using N-tier architecture.
We have the following layers:

    Application layer (Web api project) => Business Layer => Data Access Layer (Entity framework)

Here is the description of all the projects:
* JobPortal.Web - contains the web api code, sets up Ninject dependency injection container and the AutoMapper mappings,
* JobPortal.Business.Core - contains the models and the contracts of the business layer. The interfaces and models are separated into this project to ensure the dependency inversion principle, the dependent layers reference this project, not the actual implementations which are in the JobPortal.Business project,
* JobPortal.Business - contains the actual implementations for the contracts from the JobPortal.Business.Core project
* JobPortal.DataAccess.Core - contains the models and the contracts of the data access layer. The interfaces and models are separated into this project to ensure the dependency inversion principle, the dependent layers reference this project, not the actual implementations which are in the JobPortal.DataAccess project,
* JobPortal.DataAccess - contains the actual implementations for the contracts from the JobPortal.DataAccess.Core project,
* JobPortal.Common - contains common code for all the projects

#### Theming
The application uses bootstrap-sass (https://github.com/twbs/bootstrap-sass) which is a SASS powered version of Bootrstrap 3. Theming is implemented in a way that each theme should provide values for such variables like $brand-primary which is the primary color, $brand-success which is the success color, etc. To create a new theme, the following steps should be done:

* Create an scss file (for example, fancy-theme.scss) in the `client/src/app/themes` folder with the following content:

```
$icon-font-path: "~bootstrap-sass/assets/fonts/bootstrap/";
// Override bootstrap variables here
$brand-success: custom_value_here;
$brand-primary: custom_value_here;
etc...
@import "~bootstrap-sass/assets/stylesheets/bootstrap";
```

* In the `server/JobPortal.Web/Themes.json` file add an entry for the theme:
```
    {
        "id": "them_id", // should be unique string
        "is_default": true, // whether is the default theme
        "filename": "fancy-theme", // should match with the filename of the theme WITHOUT extension
        "ui_main_color": "#337ab7", // the color used when showing the theme in the app
        "ui_description": "Default theme" // the description of the theme in the ui
    }
```

* When adding a theme or modifying an existing theme, the webpack dev server and the backend api should be restarted.

#### How theming works

The webpack is configured to read the `Themes.json` file and create a separate entry for each theme. The backend api, when serving the initial html request, reads the `Themes.json` file and creates the list of themes on the `window` object. The client side accesses this list and when the theme is changed, it sets the theme link tag's href attribute to the url of the newly selected theme.