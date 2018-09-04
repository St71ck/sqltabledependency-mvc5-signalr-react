const path = require('path');

module.exports = {
    mode: 'none',
    context: path.join(__dirname, '/Scripts/react'),
    entry: "./index.js",
    output: {
        path: path.join(__dirname, '/Scripts/react/dist'),
        filename: "bundle.js"
    },
    watch: true,
    module: {
        rules: [
            {
                include: path.join(__dirname, '/Scripts/react'),
                test: /\.(js|jsx)$/,
                exclude: /(node_modules)/,
                use: {
                    loader: 'babel-loader',
                    options: {
                        presets: ['babel-preset-env', 'babel-preset-react'],
                        plugins: ['transform-class-properties']
                    }
                }
            }
        ]
    }
}