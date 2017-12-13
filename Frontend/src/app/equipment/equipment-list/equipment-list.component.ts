import {Component, Input, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';
import {EquipmentService} from '../shared/equipment.service';
import {Customer} from '../../customers/shared/customer.model';
import {Equipment} from '../shared/equipment.model';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import {CustomerService} from '../../customers/shared/customer.service';
import {switchMap} from 'rxjs/operator/switchMap';

@Component({
  selector: 'app-equipment-list',
  templateUrl: './equipment-list.component.html',
  styleUrls: ['./equipment-list.component.css']
})
export class EquipmentListComponent implements OnInit {

  @Input()
  customer: Customer;

  createEquipFormGroup: FormGroup;
  customers: Customer[];
  equipments: Equipment[];

  constructor(private equipmentService: EquipmentService, private customerService: CustomerService, private router: Router, private route: ActivatedRoute) {
    this.createEquipFormGroup = new FormGroup({
      name: new FormControl('', Validators.required)
    });


    this.customer = equipmentService.getCurrentCustomer();
    customerService.getCustomers().subscribe(Customers => this.customers = Customers);
  }

  ngOnInit() {
  this.refresh();
  }

  createNewEquipment() {
    const values = this.createEquipFormGroup.value;

    const equipment: Equipment = {
      name: values.name,
      customerId: this.customer.id
    };
    this.equipmentService.createEquipment(equipment).subscribe(
      newEquip => {
        this.customerService.getCustomerById(this.customer.id).subscribe();
        this.equipmentService.getEquipmentById(this.customer.id)
          .subscribe(Equipment => this.equipments = Equipment);
      });

  }
  refresh(){
    this.route.paramMap
      .switchMap(params => this.equipmentService.getEquipmentById(+params.get('id')))
      .subscribe(Equipment => this.equipments = Equipment);
  }
}
