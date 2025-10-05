from flask import Flask, render_template

app = Flask(__name__)

@app.route('/')
def index():
    """Renders the main single-page application."""
    return render_template('index.html')

if __name__ == '__main__':
    app.run(debug=True)