import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminPanelUserComponent } from './admin-panel-user.component';

describe('AdminPanelUserComponent', () => {
  let component: AdminPanelUserComponent;
  let fixture: ComponentFixture<AdminPanelUserComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AdminPanelUserComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AdminPanelUserComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
