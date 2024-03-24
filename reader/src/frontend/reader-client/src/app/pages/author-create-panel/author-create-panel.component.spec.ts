import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AuthorCreatePanelComponent } from './author-create-panel.component';

describe('AuthorCreatePanelComponent', () => {
  let component: AuthorCreatePanelComponent;
  let fixture: ComponentFixture<AuthorCreatePanelComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AuthorCreatePanelComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AuthorCreatePanelComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
