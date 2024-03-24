import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CategoryCreatePanelComponent } from './category-create-panel.component';

describe('CategoryCreatePanelComponent', () => {
  let component: CategoryCreatePanelComponent;
  let fixture: ComponentFixture<CategoryCreatePanelComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CategoryCreatePanelComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CategoryCreatePanelComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
