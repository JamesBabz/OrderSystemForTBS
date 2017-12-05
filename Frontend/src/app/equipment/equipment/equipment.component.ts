import {Component, Input, OnInit} from '@angular/core';
import {Employee} from '../../login/shared/employee-model';
import {Proposition} from '../../propositions/shared/proposition.model';
import {Equipment} from '../shared/equipment.model';
import {Customer} from '../../customers/shared/customer.model';
import {EquipmentService} from '../shared/equipment.service';

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

  constructor(private equipmentService: EquipmentService) { }

  ngOnInit() {
  }

}

