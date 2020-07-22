"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const arrayUtils_1 = require("helper/arrayUtils");
exports.createReducer = (params) => {
    const ADD_IDENTIFIER = `${params.actionIdentifier}_ADD`;
    const UPDATE_IDENTIFIER = `${params.actionIdentifier}_UPDATE`;
    const DELETE_IDENTIFIER = `${params.actionIdentifier}_DELETE`;
    const REPLACE_IDENTIFIER = `${params.actionIdentifier}_REPLACE`;
    const initialState = {
        data: []
    };
    const reducer = (state = initialState, action) => {
        switch (action.type) {
            case ADD_IDENTIFIER:
                const addPayloadAsArray = arrayUtils_1.ensureArray(action.payload);
                return Object.assign(Object.assign({}, state), { data: [
                        ...state.data,
                        ...addPayloadAsArray
                    ] });
            case UPDATE_IDENTIFIER:
                const updatePayloadAsArray = arrayUtils_1.ensureArray(action.payload);
                const updatedData = [...state.data];
                updatePayloadAsArray.forEach(val => {
                    const existingItemIndex = updatedData.findIndex(x => x[params.key] === val[params.key]);
                    updatedData[existingItemIndex] = val;
                });
                return Object.assign(Object.assign({}, state), { data: updatedData });
            case DELETE_IDENTIFIER:
                const deletePayloadAsArray = arrayUtils_1.ensureArray(action.payload);
                const dataToDelete = [...state.data];
                deletePayloadAsArray.forEach(val => {
                    const indexToDelete = dataToDelete.findIndex(x => x[params.key] === val[params.key]);
                    if (indexToDelete > -1) {
                        arrayUtils_1.removeAt(dataToDelete, indexToDelete);
                    }
                });
                return Object.assign(Object.assign({}, state), { data: dataToDelete });
            case REPLACE_IDENTIFIER:
                return Object.assign({}, state);
            default:
                return state;
        }
    };
    const addAction = (data) => {
        return {
            type: ADD_IDENTIFIER,
            payload: data,
        };
    };
    const updateAction = (data) => {
        return {
            type: UPDATE_IDENTIFIER,
            payload: data,
        };
    };
    const deleteAction = (data) => {
        return {
            type: DELETE_IDENTIFIER,
            payload: data,
        };
    };
    const replaceAction = (data) => {
        return {
            type: REPLACE_IDENTIFIER,
            payload: data,
        };
    };
    return {
        add: addAction,
        update: updateAction,
        delete: deleteAction,
        replace: replaceAction,
        reducer: reducer,
    };
};
//# sourceMappingURL=CrudReducer.js.map