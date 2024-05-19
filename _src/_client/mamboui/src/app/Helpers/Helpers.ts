import { userDataCookieKey } from '../Constants/Constants';
import { IUserModel } from '../Models/User/UserModel';

export const readCookieByKey = (cookieKey: string): string => {
  return decodeURI(
    document.cookie
      .match('(^|;)\\s*' + cookieKey + '\\s*=\\s*([^;]+)')
      ?.pop() || ''
  );
};
export const setCookieByKey = (
  key: string,
  value: string,
  minutes: number
): void => {
  var expires = '';
  if (minutes) {
    var date = new Date();
    date.setTime(date.getTime() + minutes * 60 * 1000);
    expires = '; expires=' + date.toUTCString();
  }
  document.cookie = key + '=' + (value || '') + expires + '; path=/';
};
export const removeCookieByKeyFromAllPaths = (key: string) => {
  var pathBits = location.pathname.split('/');
  var pathCurrent = ' path=';

  // do a simple pathless delete first.
  document.cookie = key + '=; expires=Thu, 01-Jan-1970 00:00:01 GMT;';

  for (var i = 0; i < pathBits.length; i++) {
    pathCurrent += (pathCurrent.substring(-1) != '/' ? '/' : '') + pathBits[i];
    document.cookie =
      key + '=; expires=Thu, 01-Jan-1970 00:00:01 GMT;' + pathCurrent + ';';
  }
};

export const getUserDataFromCookieAndParse = (): IUserModel | null => {
  try {
    const userDataFromCookie = readCookieByKey(userDataCookieKey);
    if (userDataFromCookie == null || userDataFromCookie == '') return null;
    var parsedUserData: IUserModel = JSON.parse(userDataFromCookie);
    if (parsedUserData == null) return null;
    return parsedUserData;
  } catch (ex) {
    return null;
  }
};

export const getUserDataFromLocalStorageAndParse = (): IUserModel | null => {
  try {
    const userDataFromLocalStorage =
      window.localStorage.getItem(userDataCookieKey);
    if (userDataFromLocalStorage == null) return null;
    var parsedUserData: IUserModel = JSON.parse(userDataFromLocalStorage);
    if (parsedUserData == null) return null;
    return parsedUserData;
  } catch (ex) {
    return null;
  }
};
