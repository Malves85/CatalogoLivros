import axios, { AxiosInstance } from "axios";
class APIServiceClass{
    private url: string =  "https://localhost:7043/api" ;
    private instance = axios.create();
    GetURL(): string {
        return this.url;
    }
    Axios(): AxiosInstance {
        return this.instance;
    }
}
export const APIService = new APIServiceClass();