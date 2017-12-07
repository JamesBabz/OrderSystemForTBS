import {Component, Input, OnInit} from '@angular/core';
import {Employee} from '../../login/shared/employee-model';
import {Proposition} from '../../propositions/shared/proposition.model';
import {Equipment} from '../shared/equipment.model';
import {Customer} from '../../customers/shared/customer.model';
import {EquipmentService} from '../shared/equipment.service';
import {ActivatedRoute, Router} from '@angular/router';
import {NgbModal} from '@ng-bootstrap/ng-bootstrap';
import {CustomerService} from '../../customers/shared/customer.service';
import {NextObserver} from 'rxjs/Observer';

@Component({
  selector: 'app-equipment',
  templateUrl: './equipment.component.html',
  styleUrls: ['./equipment.component.css']
})
export class EquipmentComponent implements OnInit {

  @Input()
  equipment: Equipment;
  @Input()
  customer: Customer;

  customers: Customer[];
  equipments: Equipment[];

  constructor(private equipmentService: EquipmentService,  private customerService: CustomerService, private router: Router, private route: ActivatedRoute, private modalService: NgbModal) {
  }

  ngOnInit() {
  }

  deleteEquipment() {
    this.equipmentService.deleteEquipmentById(this.equipment.id).subscribe(Equip =>
    this.equipmentService.getEquipmentById(this.customer.id)
      .subscribe(Equipment => this.equipments = Equipment));
  }
}

