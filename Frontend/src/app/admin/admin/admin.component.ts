import {Component, EventEmitter, Input, OnInit, Output, Sanitizer} from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';
import {FormBuilder, FormControl, FormGroup, Validators} from '@angular/forms';
import {EmployeeService} from '../../login/shared/employee.service';
import {Employee} from '../../login/shared/employee.model';
import {Customer} from '../../customers/shared/customer.model';
import {toNumber} from 'ngx-bootstrap/timepicker/timepicker.utils';
import {NotificationsService} from 'angular2-notifications/dist';


@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css']
})
export class AdminComponent implements OnInit {

  employees: Employee[];
  id: number;
  name: string;
  username: string;
  firstname: string;
  lastname: string;
  colorCode: string;
  colorDone = false;
  currentRole: string;

  modalString: string;
  employeeGroup: FormGroup;

  @Input()
  employee: Employee;
  @Output()
  eDeleted = new EventEmitter();

  private _notifiService: NotificationsService;

  constructor(private notifiService: NotificationsService, private employeeService: EmployeeService, private router: Router, private formBuilder: FormBuilder, private route: ActivatedRoute) {
    this._notifiService = notifiService;



    this.employeeGroup = this.formBuilder.group({
      id:[''],
      firstname: ['', Validators.required],
      lastname: ['', Validators.required],
      colorCode: ['', Validators.required],
      password: [Math.random().toString(36).substring(7)],
      passwordReset:[true],
      isAdmin:[true]
    });
  }

  updateEmployee() {
    const values = this.employeeGroup.value;
    const employee: Employee = {

      id: this.id,
      firstname: values.firstname,
      lastname: values.lastname,
      colorCode: values.colorCode,
    };


    this.employeeService.updateEmployeeById(this.id, employee).subscribe(Employee => {
      this.employee = Employee;
      this.showEmployees();
      this._notifiService.success("Opdateret", "Du har opdateret " + this.employee.firstname + " " + this.employee.lastname + "s data");
    });

  }

  resetPassword() {
    const values = this.employeeGroup.value;
    const employee: Employee = {
      id: this.id,
      password: values.password,
      passwordReset: values.passwordReset
    };

    console.log(employee.password);


    this.employeeService.updateEmployeeById(this.id, employee).subscribe(Employee => {
      this.employee = Employee;
      this.showEmployees();
    });

  }

  makeAdmin()
  {
    const values = this.employeeGroup.value;
    const employee: Employee = {
      id: this.id,
      isAdmin: values.isAdmin,
    };


    this.employeeService.updateEmployeeById(this.id, employee).subscribe(Employee => {
      this.employee = Employee;
      this.showEmployees();
      this._notifiService.info("Opdateret", "Du har opdateret " + this.employee.firstname + " " + this.employee.lastname + "s rolle",);
    });
    this.getInfo(this.id);
  }

  ngOnInit() {
  this.showEmployees();
  }

  createEmployee() {
    this.router.navigateByUrl('employees/create');
  }

  showEmployees() {
    this.employeeService.getEmployees().subscribe(Employees => this.employees = Employees);
  }

  getInfo(employee: Employee)
  {
    this.id = employee.id;
    this.colorCode = employee.colorCode;
    this.firstname = employee.firstname;
    this.lastname =  employee.lastname;
    this.username = employee.username;
    this.currentRole = employee.isAdmin
  }

  setColors() {
    const spans = document.getElementsByClassName('colorBox');
    for (let i = 0; i < spans.length; i++) {
      spans.item(i).setAttribute('style', 'background-color: ' + spans.item(i).getAttribute('class').substr(0, 7));
    }
    this.colorDone = true;
  }

  deleteEmployeeById() {
    this.employeeService.deleteEmployeeById(this.id).subscribe(Employee => {
      this.showEmployees();
      this.id = null;
      this._notifiService.error("Slet", "Du har slettet " + this.employee.firstname + " " + this.employee.lastname);
    });
  }

  deactivateAccount()
  {
    const values = this.employeeGroup.value;
    const employee: Employee = {

      id: this.id,
      username: "",
      firstname: values.firstname,
      lastname: values.lastname,
      colorCode: null,
      isAdmin: "Deactivated",
      password: ""
    };


    this.employeeService.updateEmployeeById(this.id, employee).subscribe(Employee => {
      this.employee = Employee;
      this.showEmployees();
      this._notifiService.error("Deaktiveret", "Du har deaktiveret " + this.employee.firstname + " " + this.employee.lastname);
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
}

