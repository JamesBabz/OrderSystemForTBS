import {Component, Input, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';
import {ReceiptService} from '../shared/receipt.service';
import {Customer} from '../../customers/shared/customer.model';
import {Receipt} from '../shared/receipt.model';
import {SharedService} from '../../shared/shared.service';

@Component({
  selector: 'app-receipt-list',
  templateUrl: './receipt-list.component.html',
  styleUrls: ['./receipt-list.component.css']
})
export class ReceiptListComponent implements OnInit {


  @Input()
  customer: Customer;

  receipts: Receipt[];

  constructor(private receiptService: ReceiptService, private router: Router, private route: ActivatedRoute, private sharedService: SharedService) {
    this.refresh();
  }

  ngOnInit() {
  }

  createReceipt() {
    this.sharedService.setCurrentCustomer(this.customer);
    this.router.navigateByUrl('/receipts/create');
  }

  refresh() {
    this.route.paramMap
      .switchMap(params => this.receiptService.getReceiptsByCustomerId(+params.get('id')))
      .subscribe(receipts => this.receipts = receipts);
  }

}
