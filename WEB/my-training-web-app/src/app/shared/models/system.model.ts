// general use models in this system
export interface APIResponse<T> {
    code: string;
    message: string;
    data: T;
}