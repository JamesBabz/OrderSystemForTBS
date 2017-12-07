import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
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
  @Output()
    eDeleted = new EventEmitter();

  modalString: string;


  customers: Customer[];
  equipments: Equipment[];

  constructor(private equipmentService: EquipmentService,  private customerService: CustomerService, private router: Router, private route: ActivatedRoute, private modalService: NgbModal) {
  }

  ngOnInit() {
  }

  deleteEquipment() {
    this.equipmentService.deleteEquipmentById(this.equipment.id).subscribe(Equip =>{
      this.eDeleted.emit(Equip);
    });

  }


  openModal(toDo: string) {
    document.getElementsByTagName('BODY')[0].classList.add('disableScroll');
    this.modalString = toDo;
  }

  /**
   * closes the modal.
   * reads css classes from the clicked element.
   * shouldKeepInput class lets the changed input stay in the fields
   * shouldClose class is to prevent child elements from closing
   * @param $event
   */
  closeModal($event) {
    if ($event.srcElement.classList.contains('shouldClose')) {
      document.getElementsByTagName('BODY')[0].classList.remove('disableScroll');
      this.modalString = '';
    }
  }

  delete($event){
    this.closeModal($event);
    this.deleteEquipment();
  }

}

