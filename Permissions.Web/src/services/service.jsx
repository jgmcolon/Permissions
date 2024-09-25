import axios from "axios";
import {confirm, loading, closeAll} from "../helpers/alerts";
import {baseApi} from "../helpers/config";


const get = (route, headers, callback, customMsj = "") => {
    if (customMsj !== "none") {
        loading(customMsj);
    }

    axios
        .get(`${baseApi()}/${route}`, {
            headers: {
                ...headers,
                "Content-Type": "application/json",
                Accept: "application/json",
            },
        })
        .then((res) => {
            closeAll()
            callback(res.data, true);

        })
        .catch((err) => {
            errAlert(err, callback);

        });
};

const post = (route, data, headers, callback, customMsj = "") => {
    if (customMsj !== "none") {
        loading(customMsj);
    }

    axios
        .post(`${baseApi()}/${route}`, data, {
            headers: {
                ...headers,
                "Content-Type": "application/json",
                Accept: "application/json",
            },
        })
        .then((res) => {
            closeAll()
            callback(res.data, true);

        })
        .catch((err) => {
            errAlert(err, callback, customMsj);
        });
};

const put = (route, data, headers, callback, customMsj = "") => {
    if (customMsj !== "none") {
        loading(customMsj);
    }

    axios
        .put(`${baseApi()}/${route}`, data, {
            headers: {
                ...headers,
                "Content-Type": "application/json",
                Accept: "application/json",
            },
        })
        .then((res) => {
            closeAll()
            callback(res.data, true);

        })
        .catch((err) => {
            errAlert(err, callback, customMsj);
        });
};

const xdelete = (route, headers, callback, customMsj = "") => {
    if (customMsj !== "none") {
        loading(customMsj);
    }

    axios
        .delete(`${baseApi()}/${route}`, {
            headers: {
                ...headers,
                "Content-Type": "application/json",
                Accept: "application/json",
            },
        })
        .then((res) => {
            closeAll()
            callback(res.data, true);

        })
        .catch((err) => {
            errAlert(err, callback);

        });
};

const errAlert = (err, callback) => {

    closeAll()

    let message = err.response?.data?.message || "";

    if(message.length  === 0)  callback(null, false);


    switch (err?.code) {

        case "ERR_BAD_RESPONSE": {
            //500;
            break;
        }

        case "ERR_BAD_REQUEST": {
            //401;
            confirm(message, "error").then(() => {
                callback(null, false);
            });

            break;
        }

        case "forbidden": {
            //403
            confirm(message, "error").then(() => {
                callback(null, false);
            });
            break;
        }

        case "NOT_FOUND": {
            //404
            confirm(message, "warning").then(() => {
                callback(null, false);
            });
            break;
        }
        default: {
            confirm(message, "error").then(() => {
                callback(null, false);
            });
        }
    }
};

export {get, post, put, xdelete};
