import * as moment from 'moment';

export interface ICookie {
    name: string;
    value: string;
    expirationDate?: string;
}

export function hasCookie(name: string): boolean {

    if (document.cookie.indexOf(name) !== -1) {
        return true;
    }

    return false;
}

export function setCookie(cookie: ICookie) {

    let cookieStringified = `${cookie.name}=${cookie.value}; expires=`;

    if (cookie.expirationDate) {
        cookieStringified += cookie.expirationDate;
    } else {
        cookieStringified += moment("Fr, 01 Dec 2100 00:00:00", "ddd, DD MMM YYYY HH:mm:ss");
    }

    document.cookie = cookieStringified;
}

export function getCookie(cookieName: string): ICookie {
    let name = cookieName + "=";
    let decodedCookie = decodeURIComponent(document.cookie);

    let returnCookie: ICookie;

    decodedCookie.split(";").forEach((cookie: string) => {

        while (cookie.charAt(0) === " ") {
            cookie = cookie.substring(1);
        }

        if (cookie.indexOf(name) === 0) {
            let cookieStringified = cookie.substring(name.length, cookie.length);
            let cookieDetailsSplit = cookieStringified.split(";");

            let cookieRet: ICookie = {
                name: cookieName,
                value: cookieDetailsSplit[0]
            }

            if (cookieDetailsSplit[1]) {
                cookieRet.expirationDate = cookieDetailsSplit[1].substring(cookieDetailsSplit[1].indexOf("=") + 1, cookieDetailsSplit[1].length);
            }

            returnCookie = cookieRet;
        }
    });

    return returnCookie;
}