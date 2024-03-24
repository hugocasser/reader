import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReadingPageComponent } from './reading-page.component';

describe('ReadingPageComponent', () => {
  let component: ReadingPageComponent;
  let fixture: ComponentFixture<ReadingPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ReadingPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ReadingPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
