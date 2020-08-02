export const usingBoolAsync = async <T = void>(stateSetter: (x: React.SetStateAction<boolean>) => void, action: () => Promise<T>) => {

    try {
        stateSetter(true);

        return await action();
    }
    finally {
        stateSetter(false);
    }
}