import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ListdoComponent } from './listdo.component';

describe('ListdoComponent', () => {
  let component: ListdoComponent;
  let fixture: ComponentFixture<ListdoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ListdoComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ListdoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
