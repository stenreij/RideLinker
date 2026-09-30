module.exports = {
    preset: 'jest-preset-angular',
    globalSetup: 'jest-preset-angular/global-setup',
    setupFilesAfterEnv: ['<rootDir>/src/setup.jest.ts'],
    transform: {
        '^.+\\.(ts|js|html)$': 'jest-preset-angular',
    },
    globals: {
        'ts-jest': {
            tsconfig: './tsconfig.spec.json',
            stringifyContentPathRegex: '\\.html$',
            astTransformers: ['jest-preset-angular/InlineHtmlStripStylesTransformer'],
            isolatedModules: true,
            preserveSymlinks: true,
        },
    },
    moduleFileExtensions: ['ts', 'html', 'js', 'json'],
    moduleDirectories: ['node_modules', 'src'],
    modulePaths: ['<rootDir>'],
    moduleNameMapper: {
        '^@app/(.*)$': ['<rootDir>/src/app/$1'],
    },
};