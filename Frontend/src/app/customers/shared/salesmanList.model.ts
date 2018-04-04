import {Customer} from './customer.model';
import {Employee} from '../../login/shared/employee.model';

export class SalesmanList {
  id?: number;
  customer?: Customer;
  customerId: number;
  employee?: Employee;
  employeeId: number;

}
