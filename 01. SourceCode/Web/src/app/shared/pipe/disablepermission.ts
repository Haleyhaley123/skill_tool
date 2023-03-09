import { Pipe } from '@angular/core';

@Pipe({
    name: 'disablePermission',
    pure: false
})
export class DisablePermission {
    transform(permission: any): boolean {
        var isAuthorize = true;
        var user = localStorage.getItem('pcmtCurrentUser');
        var listPermission: any[] = [];
        if (user) {
            let userString = localStorage.getItem('pcmtCurrentUser');
            var listPermission: any[] = [];
            if (userString)
                listPermission = JSON.parse(userString).permissions;
            if (listPermission != null && listPermission.length > 0 && permission) {
                permission.forEach(function (item: any) {
                    if (listPermission.indexOf(item) != -1) {
                        isAuthorize = false;
                    }
                });
            }
        }

        // if (!permission || permission.length == 0) {
        //     isAuthorize = false;
        // }

        // if (!isAuthorize) {
        //     isAuthorize = true;
        // }

        return isAuthorize;
    }
}