import { fakeAsync, TestBed, ComponentFixture} from "@angular/core/testing";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { HttpClientTestingModule } from "@angular/common/http/testing";
import { NewStoriesComponent } from "./new-stories.component";
import { NewStoriesService } from "./new-stories.component.service";
import { MatCardModule, MatTableModule } from "@angular/material";

describe("Component: New Stories", () => {

    let component: NewStoriesComponent;
    let fixture: ComponentFixture<NewStoriesComponent>;

    beforeEach(() => {
      TestBed.configureTestingModule({
        declarations:
        [
          NewStoriesComponent
        ],
        imports: [
          BrowserAnimationsModule,
          HttpClientTestingModule,
		  MatCardModule,
		  MatTableModule
        ],
        providers: [
          NewStoriesService,
          { provide: "BASE_URL", useValue: "https://localhost:44314" }
        ]
      });

      fixture = TestBed.createComponent(NewStoriesComponent);
      component = fixture.componentInstance;
      fixture.detectChanges();
    });

    it("should be created",
		  fakeAsync(() => {
			expect(NewStoriesComponent).toBeTruthy();
		  })
	  );

	 it("should render title in a h1 tag",
		 fakeAsync(() => {
		   const titleText = fixture.nativeElement.querySelector("h1").textContent;
		   expect(titleText).toEqual("Newest Hacker Stories");
		 })
	 );

	 it("expects component to render No Stories Found when no result",
		fakeAsync(() =>{
		   const noRecords = fixture.nativeElement.querySelector("mat-card").textContent;
		   expect(noRecords).toEqual("No Stories Found");
		})
	 );

	 it("expects component to render Table when has result",
		fakeAsync(() =>{
		   const noRecords = fixture.nativeElement.querySelector("mat-table").textContent;
		   expect(noRecords).toEqual("No Stories Found");
		})
	 );
});
