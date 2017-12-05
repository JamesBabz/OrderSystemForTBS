import {Component, Input, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';
import {EquipmentService} from '../shared/equipment.service';
import {Customer} from '../../customers/shared/customer.model';
import {Equipment} from '../shared/equipment.model';

@Component({
  selector: 'app-equipment-list',
  templateUrl: './equipment-list.component.html',
  styleUrls: ['./equipment-list.component.css']
})
export class EquipmentListComponent implements OnInit {

  @Input()
  customer: Customer;

  equipments: Equipment[];

  constructor(private equipmentService: EquipmentService, private router: Router, private route: ActivatedRoute) {
    this.route.paramMap
      .switchMap(params => this.equipmentService.getEquipmentById(+params.get('id')))
      .subscribe(Equipment => this.equipments = Equipment);
  }


  ngOnInit() {
  }

}
