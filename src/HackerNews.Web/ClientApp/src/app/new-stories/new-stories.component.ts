import {Component, ViewChild} from "@angular/core";
import {  NewStoriesService, NewStory  } from "./new-stories.component.service";
import { MatTableDataSource, MatPaginator } from "@angular/material";

@Component({
  selector: 'app-new-stories-component',
  templateUrl: './new-stories.component.html',
  styleUrls: ['./new-stories.component.css']
})

export class NewStoriesComponent{
	title = 'Newest Hacker Stories';
	noRecords = "No Stories Found";
	dataSource = new MatTableDataSource<NewStory>();
	displayedColumns: string[] =
		[
		  "title",
      "author"
		];

  @ViewChild(MatPaginator)
  set matPaginator(paginator: MatPaginator) {
    this.dataSource.paginator = paginator;
  }
	 constructor(private readonly newStoriesService: NewStoriesService) {
	   this.getStories()
	 }

   getStories() {
    this.newStoriesService
      .getNewestStories()
      .subscribe( (result: NewStory[]) => {
        this.dataSource =  new MatTableDataSource<NewStory>(result);
      }
    );

  }
}
