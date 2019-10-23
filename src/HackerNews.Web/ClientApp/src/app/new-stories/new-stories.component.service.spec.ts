import { NewStoriesService, NewStory } from "./new-stories.component.service";
import { TestBed, tick, fakeAsync, inject } from "@angular/core/testing";
import { HttpClientTestingModule, HttpTestingController } from "@angular/common/http/testing";

describe("Service: New Stories", () => {

  const baseUri = "https://localhost:44314/";
  let results = [];
  const response = { results };

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports:
      [
        HttpClientTestingModule
      ],
      providers:
      [
        NewStoriesService,
        { provide: "BASE_URL", useValue: baseUri }
      ]
    });

    for (let i = 0; i < 6; i++) {
		  if (i === 3) {
			results.push(new NewStory(
			   i,
			  `test title ${i}`,
			  `test author ${i}`
			  ));
		  } else {
			results.push(new NewStory(
			   i,
			  `test title ${i}`,
			  `test author ${i}`
			  ));
		  }
      }
  });

  afterEach(inject([HttpTestingController],
    (httpMock: HttpTestingController) => {
      httpMock.verify();
        results = [];
    }));

  it("should be created", fakeAsync(() => {
    expect(NewStoriesService).toBeTruthy();
  }));

  it("expects service to fetch collection of newest stories",
    fakeAsync(inject([HttpTestingController, NewStoriesService],
      (httpMock: HttpTestingController, service: NewStoriesService) => {

        service.getNewestStories().subscribe(
            newStoriesService => {
              expect(newStoriesService.length).toBe(6);
              expect(newStoriesService[3].id).toBe(3);
              expect(newStoriesService[3].title).toBe("test title 3");
              expect(newStoriesService[3].author).toBe("test author 3");
            });

          const req = httpMock.expectOne(`${baseUri}HackerNews/stories/GetStories`);
          expect(req.request.method).toEqual("GET");
			    req.flush(response.results);
          tick();
        })
    ));

  it("expects service to fetch empty collection of newest stories",
    fakeAsync(inject([HttpTestingController, NewStoriesService],
        (httpMock: HttpTestingController, service: NewStoriesService) => {
          service.getNewestStories().subscribe(
            newStoriesService => {
              expect(newStoriesService.length).toBe(0);
            });
          const req = httpMock.expectOne(`${baseUri}HackerNews/stories/GetStories`);

          expect(req.request.method).toEqual("GET");
          tick();
        })
    ));
});
