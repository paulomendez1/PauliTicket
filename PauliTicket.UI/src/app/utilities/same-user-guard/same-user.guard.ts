import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import Swal from 'sweetalert2';

@Injectable({
    providedIn: 'root'
})
export class RequireSameUserAsRequestRouteGuard implements CanActivate {

    public id!: string | null;

    constructor(private authService: AuthService,
        private router: Router) { }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
        this.id = route.paramMap.get('id');
        return this.checkSameUser(this.id);
    }

    checkSameUser(id: string | null) {
        if (id) {
            if (this.authService.isAuthenticated() && this.authService.isTheSameUser(id)) {
                return true;
            }
            else {
                Swal.fire('Error', 'Oops, You dont have permission to access here', "error").then(() => this.router.navigate(['/']));
                return false;
            }
        }
        else {
            return false;
        }


    }
}
