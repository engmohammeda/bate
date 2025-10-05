import webbrowser
from threading import Timer
from flask import Flask, render_template

app = Flask(__name__)

# --- Routes ---
@app.route('/')
def index():
    """Renders the home page."""
    return render_template('index.html')

@app.route('/about')
def about():
    """Renders the about page."""
    return render_template('about.html')

@app.route('/contact')
def contact():
    """Renders the contact page."""
    return render_template('contact.html')

def open_browser():
    """Opens the default web browser to the application's URL."""
    webbrowser.open_new('http://127.0.0.1:5000/')

if __name__ == '__main__':
    # Opens the browser 1 second after the app starts
    Timer(1, open_browser).start()
    app.run(debug=True)