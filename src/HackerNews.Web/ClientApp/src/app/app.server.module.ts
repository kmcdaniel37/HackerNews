import { NgModule } from '@angular/core';
import { ServerModule } from '@angular/platform-server';
import { ModuleMapLoaderModule } from '@nguniversal/module-map-ngfactory-loader';
//import { AppComponent } from './app.component';
import { NewStoriesComponent } from "./new-stories/new-stories.component";
import { AppModule } from './app.module';

@NgModule({
    imports: [AppModule, ServerModule, ModuleMapLoaderModule],
  bootstrap: [NewStoriesComponent]
})
export class AppServerModule { }
