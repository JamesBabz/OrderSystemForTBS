import {Customer} from '../../customers/shared/customer.model';
import {Employee} from '../../login/shared/employee-model';

export class Proposition {
  id?: number;
  title: string;
  description: string;
  creationDate: string;
  customer: Customer;
  customerId: number;
  employee: Employee;
  EmployeeId: number;
  fileId: number;
}
