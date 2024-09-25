import Swal from "sweetalert2";
import withReactContent from "sweetalert2-react-content";

const xSwal = withReactContent(Swal);

const OKBUTTON = "OK";
const iconHtml = "success";

export const confirm = (
    customMessage,
    icon = iconHtml,

    customOkbutton = OKBUTTON
) => {
    return xSwal.fire({
        html: customMessage,
        confirmButtonText: customOkbutton,
        icon: icon,
        footer: "",
    });
};

export const toast = (customMessage, customTitle = "", iconHtml = iconHtml) => {
    const Toast = Swal.mixin({
        showConfirmButton: false,
        toast: true,
        position: "top-end",
        timer: 2000,
        timerProgressBar: true,
        didOpen: (toast) => {
            toast.addEventListener("mouseenter", Swal.stopTimer);
            toast.addEventListener("mouseleave", Swal.resumeTimer);
        },
    });

    return Toast.fire(customTitle, customMessage, iconHtml);
};

export const loading = (customMessage = "", customTitle = "") => {
    return xSwal.fire({
        title: customTitle,
        html: customMessage,
        allowOutsideClick: false,
        didOpen: () => {
            Swal.showLoading();
        },
    });
}

export const closeAll = () => {
    xSwal.close();
}


