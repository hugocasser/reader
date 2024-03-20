import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BooksListItemComponent } from './books-list-item.component';

describe('BooksListItemComponent', () => {
  let component: BooksListItemComponent;
  let fixture: ComponentFixture<BooksListItemComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BooksListItemComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(BooksListItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
