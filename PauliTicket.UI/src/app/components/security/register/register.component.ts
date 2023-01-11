import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { userCredentials } from 'src/app/models/security';
import { parseWebAPIErrors } from 'src/app/utilities/shared.functions';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
  constructor(private authService: AuthService,
    private router: Router) { }

  errors: string[] = []
  ngOnInit(): void {
  }

  register(userCredentials: userCredentials) {
    this.errors = [];
    this.authService.register(userCredentials).subscribe(authenticationResponse => {
      Swal.fire('Success', 'You have been registered!, Please check your mailbox to confirm your account!', "success").then(() => this.router.navigate(['/']));
    }, error => this.errors = parseWebAPIErrors(error));
  }
}
