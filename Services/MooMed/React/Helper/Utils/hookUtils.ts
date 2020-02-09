export const usingBoolAsync = async (stateSetter: (x: React.SetStateAction<boolean>) => void, action: () => Promise<void>) => {

    try {
        stateSetter(true);

        await action();
    }
    finally {
        stateSetter(false);
    }
}