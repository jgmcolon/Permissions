import {post, get, put} from "./service";

const controller = "PermissionType"

export const List = (callback) => {
    get(`v1/${controller}/list`, {},  callback, "none");
};
export const GetAll = (CurrentPage, PerPage, callback) => {
    get(`v1/${controller}`, {page: CurrentPage, perPage: PerPage},  callback, "none");
};

export const GetBy = (Id, callback) => {
    get(`v1/${controller}/${Id}`, {},  callback, `Cargando detalle compañía`);
};

export const Insert = (dto, callback) => {
    post(`v1/${controller}`, dto, {}, callback, `Insertando compañía ${dto["employeeName"]}` );
};

export const Update = (Id, dto, callback) => {
    put(`v1/${controller}/${Id}`, dto, {}, callback, `Actualizando compañía ${dto["employeeName"]}`);
};


