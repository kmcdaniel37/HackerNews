import { Injectable, Inject } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";

export class NewStory {
  constructor(
    public id = 0,
    public title = "",
    public author = ""
	){}

  clone() {
    return new NewStory(
      this.id,
      this.title,
      this.author
	  );
  }
}

@Injectable()
export class NewStoriesService {
  results: NewStory[];
  baseUrl: string;

  constructor(private readonly http: HttpClient, @Inject("BASE_URL") baseUrl: string) {
    this.results = [];
    this.baseUrl = baseUrl;
  }

   getNewestStories(): Observable<NewStory[]>{
     return this.http.get<NewStory[]>(`${this.baseUrl}HackerNews/stories/GetStories`);
   }
}
