import {Customer} from '../../customers/shared/customer.model';
import {Employee} from '../../login/shared/employee.model';


export class Visit {
  id?: number;
  title: string;
  description: string;
  dateTimeOfVisitStart: Date;
  dateTimeOfVisitEnd: Date;
  canceled: boolean;
  customer?: Customer;
  customerId?: number;
  employee?: Employee;
  employeeId: number;
  progressPart: number;
}
