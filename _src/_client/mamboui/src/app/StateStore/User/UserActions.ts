export const UserStateActions = createActionGroup({
  source: 'Users',
  events: {
    UserLoggedIn: props<{ loggedInUser: UserModel }>(),
    UserLoggedOut: props<any>(),
  },
});
