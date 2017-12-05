import {Customer} from '../../customers/shared/customer.model';

export class Equipment {
  id?: number;
  name: string;
  customer: Customer;
  customerId: number;
}
