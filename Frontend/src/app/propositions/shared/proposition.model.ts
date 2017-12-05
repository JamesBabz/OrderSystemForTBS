import {Customer} from '../../customers/shared/customer.model';
import {Employee} from '../../login/shared/employee.model';
import {DateFormatter} from '@angular/common/src/pipes/intl';

export class Proposition {
  id?: number;
  title: string;
  description: string;
  creationDate: Date;
  customer?: Customer;
  customerId: number;
  employee?: Employee;
  EmployeeId: number;
  fileId: number;

}
