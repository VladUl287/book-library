export const getUrlParams = (form: object): URLSearchParams => {
    const init: Record<string, string> = {}
    for (const [key, value] of Object.entries(form)) {
        init[key] = value.toString();
    }
    return new URLSearchParams(init);
}

export const getFormData = (form: object) => {
    const formData = new FormData();
    for (const [key, value] of Object.entries(form)) {
        formData.append(key, value);
    }
    return formData;
}