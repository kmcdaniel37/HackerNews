import { BrowserModule, HAMMER_LOADER } from "@angular/platform-browser";
import { NgModule } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { HttpClientModule } from "@angular/common/http";
import { RouterModule } from "@angular/router";

import { MatCardModule, MatTableModule, MatPaginatorModule } from "@angular/material";
import { NewStoriesComponent } from "./new-stories/new-stories.component";
import { NewStoriesService } from "./new-stories/new-stories.component.service";

@NgModule({
  declarations: [
    NewStoriesComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule, MatCardModule, MatTableModule, MatPaginatorModule,
    RouterModule.forRoot([
      { path: "", component: NewStoriesComponent, pathMatch: "full" }
    ])
  ],
  providers: [
    NewStoriesService,
    { provide: HAMMER_LOADER, useValue: () => new Promise(() => { }) }
  ],
  bootstrap: [NewStoriesComponent]
})
export class AppModule { }
