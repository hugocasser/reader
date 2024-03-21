import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminPanelGroupComponent } from './admin-panel-group.component';

describe('AdminPanelGroupComponent', () => {
  let component: AdminPanelGroupComponent;
  let fixture: ComponentFixture<AdminPanelGroupComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AdminPanelGroupComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AdminPanelGroupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
