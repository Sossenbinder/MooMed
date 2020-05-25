export const formatTranslation = (translation: string, ...formatPastes: Array<string>) => {
	let toFormat = translation;

	for (let i = 0; i < formatPastes.length; ++i)
	{
		toFormat = toFormat.replace(`{${i}}`, formatPastes[i]);
	}

	return toFormat;
}