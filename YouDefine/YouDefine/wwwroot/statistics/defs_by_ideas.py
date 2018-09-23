from plotly.offline import download_plotlyjs, init_notebook_mode, plot, iplot
from plotly.graph_objs import Scatter, Layout, Figure, Bar
from random import randint, uniform

def get_data_from_file(file_name):
    file_data = []
    try:
        file = open(file_name, "r+")
        for line in file:
            file_data.append(line)
        file.close()
        return file_data
    except IOError:
        print("Could not open "+file_name)
        return False

def get_rgba_array_colors(length):
	colors = []
	for i in range(length):
		r = str(randint(0,255))
		g = str(randint(0,255))
		b = str(randint(0,255))
		a = str(uniform(0.8, 1))
		color = 'rgba(' + r + ',' + g + ',' + b + ',' + a + ')'
		colors.append(color)
	return colors

def main():
    data_x = get_data_from_file("data/definitions_by_ideas_titles.txt")
    data_y = get_data_from_file("data/definitions_by_ideas_texts.txt")
    
    if not data_x or not data_y:
        return
    
    x_axis = data_x

    # Create a trace
    trace0 = Bar(
        x=data_x,
        y=data_y,
        name='ideas',
		marker=dict(
        color=get_rgba_array_colors(len(data_x))
		),
    )

    layout = Layout(
        yaxis=dict(
            title='no of definitions',
            range=[1, 6]
        ),
        xaxis=dict(
            title='idea titles',
        )
    )
   
    plot(Figure(data=[trace0], layout=layout), filename='definitions_by_ideas.html')

if __name__ == "__main__":
    main()
