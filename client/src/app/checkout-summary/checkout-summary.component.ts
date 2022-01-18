import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { pizza } from '../_models/pizza';
import { AccountService } from '../_services/account.service';
import { CartService } from '../_services/cart.service';

@Component({
  selector: 'app-checkout-summary',
  templateUrl: './checkout-summary.component.html',
  styleUrls: ['./checkout-summary.component.css']
})
export class CheckoutSummaryComponent implements OnInit {

itemsinCart :number;
products : pizza[];

constructor(private cartSvc: CartService, public accountService: AccountService) {}
//Assign data returned from our Observable to a Property called products
ngOnInit()
{
  this.cartSvc.cartItems.subscribe(d => {
    //Get total amount of items in cart
    this.itemsinCart = d.length;
  })

  this.cartSvc.cartItems.subscribe(cartitems => {
    //Get all items in cart
    this.products = cartitems;
  })
}

_emptyCart()
{
  this.cartSvc.emptyCart();
}
_removeCartItem(id: number)
{
  this.cartSvc.removeCartItem(id);
}
_makePurchase()
{
  this.cartSvc.makePurchase();
}

}
