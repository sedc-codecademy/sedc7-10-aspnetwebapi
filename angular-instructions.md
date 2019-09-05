# Create your first Angular application


* install angluar cli :  `npm install -g @angular/cli`
* create angluar boiler plate app :  `ng new your-app-name`
* navigate into your new project :  `cd your-app-name`
* start the angular application :  `ng serve`

#### You have your application up and running !!

## Create your first custom component


* run the following command in your root application directory :  `ng generate component yourComponentName`


### Start development


* create the components
* install bootstrap and its dependecies :  `npm i bootstrap jquery popper.js`
* configure angular.json, add :  `"styles": [
              "src/styles.scss",
              "node_modules/bootstrap/dist/css/bootstrap.min.css"
            ],
            "scripts": [
              "node_modules/jquery/dist/jquery.min.js",
              "node_modules/bootstrap/dist/js/bootstrap.min.js"
            ]`
* edit app.component.html and add :  `<app-nav></app-nav>
<router-outlet></router-outlet>`
* add the routes in app-routing.module.ts
* import and export modules in app-routing.module.ts

 // configure api
* check connection-string
* configure Startup.cs
* configure ssl in WebApi project
* use the port from the ssl in the angular application
