import webbrowser
from threading import Timer
from flask import Flask, render_template

app = Flask(__name__)

# --- Routes ---
@app.route('/')
def index():
    """Renders the home page (landing page)."""
    return render_template('index.html')

@app.route('/about')
def about():
    """Renders the about page."""
    return render_template('about.html')

@app.route('/contact')
def contact():
    """Renders the contact page."""
    return render_template('contact.html')

@app.route('/projects')
def projects():
    """Renders the projects page."""
    return render_template('projects.html')

@app.route('/project/<project_id>')
def project_detail(project_id):
    """Renders a single project detail page."""
    # In a real app, you'd fetch project data based on the ID
    return render_template('project_detail.html', project_id=project_id)

@app.route('/services')
def services():
    """Renders the services page."""
    return render_template('services.html')

@app.route('/experience')
def experience():
    """Renders the experience and certifications page."""
    return render_template('experience.html')

@app.route('/technologies')
def technologies():
    """Renders the technologies and tools page."""
    return render_template('technologies.html')

@app.route('/gallery')
def gallery():
    """Renders the gallery page."""
    return render_template('gallery.html')

@app.route('/blog')
def blog():
    """Renders the blog page."""
    return render_template('blog.html')

@app.route('/testimonials')
def testimonials():
    """Renders the testimonials page."""
    return render_template('testimonials.html')

def open_browser():
    """Opens the default web browser to the application's URL."""
    webbrowser.open_new('http://127.0.0.1:5000/')

if __name__ == '__main__':
    # Opens the browser 1 second after the app starts
    Timer(1, open_browser).start()
    app.run(debug=True)