export interface Login{
  email: string,
  password: string
}

export interface SignUp
{
  userName: string,
  email: string,
  mobileNumber: string,
  password: string
}

export interface DealerSignUp{

    name: string,
    email: string,
    phoneNo: string,
    password: string
}

export interface LoginResponse{
  userID:string,
  userName: string,
  email: string,
  access: string,
  accessToken:string
}