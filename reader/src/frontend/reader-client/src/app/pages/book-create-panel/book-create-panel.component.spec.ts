import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BookCreatePanelComponent } from './book-create-panel.component';

describe('BookCreatePanelComponent', () => {
  let component: BookCreatePanelComponent;
  let fixture: ComponentFixture<BookCreatePanelComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BookCreatePanelComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(BookCreatePanelComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
