import {post, get, put} from "./service";

const controller = "permission"

export const GetAll = (CurrentPage, PerPage, callback) => {
    get(`v1/${controller}/list`, {page: CurrentPage, perPage: PerPage},  callback, "none");
};

export const GetBy = (Id, callback) => {
    get(`v1/${controller}/${Id}`, {},  callback, `Cargando Permiso`);
};

export const Request = (dto, callback) => {
    post(`v1/${controller}/Request`, dto, {}, callback, `Insertando Permiso` );
};

export const Modify = (Id, dto, callback) => {
    put(`v1/${controller}/Modify/${Id}`, dto, {}, callback, `Actualizando Permiso`);
};


