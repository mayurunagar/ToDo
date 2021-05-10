import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AccountService } from '../services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  providers: [AccountService]
})
export class RegisterComponent {
  email: string;
  password: string;
  confirmPassword: string;

  constructor(http: HttpClient, public accountService: AccountService, private router: Router) {

  }

  register() {
    this.accountService.register({ email: this.email, password: this.password, confirmPassword: this.confirmPassword }).subscribe((result: any) => {
      if (result) {
        this.router.navigate(['/login']);
      }
    }, error => console.log(error));
  }
}
