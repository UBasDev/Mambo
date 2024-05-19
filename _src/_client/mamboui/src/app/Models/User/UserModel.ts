export interface IUserModel {
  username: string;
  email: string;
  firstname: string;
  lastname: string;
  roleName: string;
  roleLevel: number;
  screens: ReadonlyArray<string>;
}

export default class UserModel implements IUserModel {
  constructor() {
    this.username = '';
    this.email = '';
    this.firstname = '';
    this.lastname = '';
    this.roleName = '';
    this.roleLevel = 0;
    this.screens = [];
  }
  username: string;
  email: string;
  firstname: string;
  lastname: string;
  roleName: string;
  roleLevel: number;
  screens: ReadonlyArray<string>;
}
