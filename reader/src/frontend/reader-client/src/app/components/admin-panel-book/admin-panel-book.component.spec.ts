import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminPanelBookComponent } from './admin-panel-book.component';

describe('AdminPanelBookComponent', () => {
  let component: AdminPanelBookComponent;
  let fixture: ComponentFixture<AdminPanelBookComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AdminPanelBookComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AdminPanelBookComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
